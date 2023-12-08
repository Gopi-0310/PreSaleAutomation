
export interface CostByResource {
  id: number;
  activity: string;
  parentId: number;
  description: string;
  hours: number;
  week: string;
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
  rate: {
    id   : string;
    rate : string
  }
}
