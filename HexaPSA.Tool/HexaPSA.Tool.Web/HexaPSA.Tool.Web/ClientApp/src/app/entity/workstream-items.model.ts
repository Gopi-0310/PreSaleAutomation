import { workStream } from "./workstream-activity";

export interface workStreamItems {
  id: number;
  activity: string;
  parentId: number;
  description: string;
  children?: workStreamItems[];
  hours: number;
  week: string;
  roles: string;
  role: {
    code: string;
    id: string;
    name: string;
  };
  workStream: {
    projectId: string;
    id: string;
    name: string;
  }
}
