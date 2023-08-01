using System.Reflection;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit.Abstractions;

namespace beer_directory.Tests.Integration;

public class BlanketAuthorizationTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private const string Controller = nameof(Controller);
    private const string RootNamespace = nameof(beer_directory);
    private static readonly Type ControllerType = typeof(ControllerBase);
    private static readonly string[] ExecutableExtensions = { ".exe", ".dll" };
    private static readonly Type PageType = typeof(PageModel);

    public BlanketAuthorizationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void AllActionsOrParentControllerHaveAuthorizationAttributeTest()
    {
        LoadAllAssemblies();
        var allControllers = GetAllControllerTypes();

        Console.WriteLine($"Found {allControllers.Count} controllers/pages");

        var unauthorizedControllers =
            GetControllerTypesThatAreMissing<AuthorizeAttribute>(allControllers);

        unauthorizedControllers =
            GetControllerTypesThatAreMissing<AllowAnonymousAttribute>(unauthorizedControllers);

        //allControllers.Count.Should().BeGreaterThan(unauthorizedControllers.Count);

        var notAuthorizedEndpoints = 0;

        foreach (var controller in unauthorizedControllers)
        {
            var actions =
                controller.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                    .Where(m => m.MemberType == MemberTypes.Method && !m.IsSpecialName)
                    .ToList();

            var unauthorizedActions =
                actions.Where(
                        action =>
                            action.GetCustomAttribute<AuthorizeAttribute>() == null &&
                            action.GetCustomAttribute<AllowAnonymousAttribute>() == null)
                    .ToList();

            if (unauthorizedActions is null || unauthorizedActions.Count == 0)
            {
                continue;
            }

            unauthorizedActions.ForEach(
                action => _testOutputHelper.WriteLine($"{controller.FullName}.{action.Name} is unauthorized!"));

            notAuthorizedEndpoints += unauthorizedActions.Count;
        }
        notAuthorizedEndpoints.Should().Be(0, $"all endpoints should have [Authorize] or [AllowAnonymous]");
    }

    private static List<Type> GetAllControllerTypes()
    {
        var assemblies = AppDomain.CurrentDomain
            .GetAssemblies();

        var namespaceVariants = new[] { RootNamespace, RootNamespace.Replace("_", "-") };

        var types = new List<Type>();

        foreach (var variant in namespaceVariants)
        {
            types.AddRange(
                assemblies
                    .Where(a => a.FullName!.StartsWith(variant))
                    .SelectMany(a => a.GetTypes()
                        .Where(t => t.FullName!.Contains(Controller) ||
                                    t.BaseType == ControllerType ||
                                    t.DeclaringType == ControllerType ||
                                    t.BaseType == PageType ||
                                    t.DeclaringType == PageType))
                    .ToList());
        }

        return types;
    }

    private static List<Type> GetControllerTypesThatAreMissing<TAttribute>(
        IEnumerable<Type> types)
        where TAttribute : Attribute
        => types.Where(t => t.GetCustomAttribute<TAttribute>() == null)
            .ToList();

    private static bool IsExeOrDll(string path)
        => ExecutableExtensions.Any(
            extension =>
                extension.Equals(
                    Path.GetExtension(path),
                    StringComparison.OrdinalIgnoreCase));

    private static T TryCatchIgnore<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch
        {
            return default!;
        }
    }

    private static void LoadAllAssemblies()
    {
        var namespaceVariants = new[] {RootNamespace, RootNamespace.Replace("_", "-")};

        var allAssemblies = new List<Assembly>();

        foreach (var variant in namespaceVariants)
        {

            var assemblies =
                Directory.EnumerateFiles(
                        Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                        $"{variant}.*")
                    .Where(IsExeOrDll)
                    //.Select(a => TryCatchIgnore(() => Assembly.LoadFrom(a)))
                    .Select(_ =>
                        TryCatchIgnore(
                            () => AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(_))))
                    .ToList();

            allAssemblies.AddRange(assemblies);
        }

        allAssemblies = allAssemblies.Where(x => x is not null).ToList();

        allAssemblies.Should().NotBeNullOrEmpty();
    }
}