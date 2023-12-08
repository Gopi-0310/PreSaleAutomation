export interface workStream {
  id: number;
  activity: string;
  parentId: number;
  description: string;
  children?: workStream[];
  eta: number;
  weeks: string;
  roles:string;
}
