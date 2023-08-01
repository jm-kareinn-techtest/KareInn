import { Style } from "./style"

export interface Beer {
    id: string
    name: string
    style: Style
    brewer: string
    alcoholByVolume: number
}