export interface workStream {
  id: any;
  activity: string;
  parentId: number;
  description: string;
  children?: workStream[];
  eta: number;
  weeks: string;
  roles: string;
}
