import { Role } from "./role.model"

export interface CapacityUtilizationMapping {
  id         ?: string
  projectId  : string
  roleId     : Role 
  hours     ?: number
  rate       : number
  location   : string
}
