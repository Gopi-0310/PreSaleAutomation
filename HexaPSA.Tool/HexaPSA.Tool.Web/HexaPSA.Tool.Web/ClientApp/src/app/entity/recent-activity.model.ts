
import { entitybase } from "./entity-base";

export interface RecentActivityLog extends entitybase {
  projectId?: string;
  activity: string;
}
