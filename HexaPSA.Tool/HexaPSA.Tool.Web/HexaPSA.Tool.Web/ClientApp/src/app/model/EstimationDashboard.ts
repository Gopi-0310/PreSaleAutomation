export interface EstimationDashboard {
    id                ?: number
    projectName       : string 
    projectType       : string 
    technology        : [] 
    startDate         : Date 
    endDate           : Date 
    progress?         : string 
    addEstimate       :[{ userName : string 
                          roles    : string }]

                         
}
