import { entitybase } from "./entity-base";

export interface ActivityLog extends entitybase {
  id?: number;
  projectId?: string;
  logActivity: string;
}
