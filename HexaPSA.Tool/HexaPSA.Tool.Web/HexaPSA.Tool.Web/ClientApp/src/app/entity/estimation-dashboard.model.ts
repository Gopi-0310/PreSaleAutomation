export interface EstimationDashboard {
    id                  : string
    name                : string
    projectTypeId        : string
    effectiveStartDate  : any
    effectiveEndDate    : any
    estimatedUsers      : estimatedUser[]
    technologies        : technology[]
}
interface ProjectType {
  id: string;
  name: string;
}

interface estimatedUser {
  userId: any;
  roleId: any;
}

interface technology {
  id    : any;
  name  : any;
}
