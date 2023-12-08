import { NumberFormat } from "xlsx";

export interface CapacityUtilization {
    id       ?: string,
    roleId    : string,
    projectId : string,
    hours     : number,
    rate     ?: number,
    location  :string
}
