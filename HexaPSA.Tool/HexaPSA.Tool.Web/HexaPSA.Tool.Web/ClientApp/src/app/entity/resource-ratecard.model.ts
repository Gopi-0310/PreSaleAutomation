import { Role } from "./role.model"

export interface ResourceRatecard {
    id    ?: string
    rate   : number
    roleId : Role 
   
}
