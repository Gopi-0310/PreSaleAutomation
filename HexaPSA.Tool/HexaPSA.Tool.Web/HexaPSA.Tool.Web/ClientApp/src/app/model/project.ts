export interface CreateProjectRequest {
  id: string;
  name: string;
  projectTypeId: string;
  effectiveStartDate: string;
  effectiveEndDate: string;
  estimatedUsers: UserRoleRequest[];
  technology: TechnologyRequest[];
}

export interface UserRoleRequest {
  userId: string;
  roleId: string;
}

export interface TechnologyRequest {
  id: string;
  name: string;
}
