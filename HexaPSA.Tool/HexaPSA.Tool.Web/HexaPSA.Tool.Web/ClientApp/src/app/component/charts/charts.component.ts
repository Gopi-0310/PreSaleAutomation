import { Component, Input, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData, ChartOptions, ChartType } from 'chart.js';
import 'chartjs-adapter-date-fns' //required for chart.js date adapter
// npm install date-fns chartjs-adapter-date-fns --save
import { CapacityUtilizationService } from '../../services/capacity-utilization.service';
import { ApiIntractionsService } from '../../services/api-intractions.service';
import { environment } from '../../../environments/environment';
import { ProjectService } from '../../services/project.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.scss']
})
export class ChartsComponent implements OnInit {

  projectId : any;
  capacityList: any;
  capacityXaxis: number[] = [];
  capacityYaxis: string[] = [];
  pieChartList: any;
  pieXaxis: number[] = [];
  pieYaxis: string[] = [];
  ganttChartList: any;
  ganttXaxis: string[] = [];
  ganttYaxis: any[] = [];
  ganttcolors: string[] = [];
  projectStartDate?: string;

  setColors = [
    'rgba(255, 26, 104, 1)',
    'rgba(54, 162, 235, 1)',
    'rgba(255, 206, 86, 1)',
    'rgba(75, 192, 192, 1)',
    'rgba(153, 102, 255, 1)',
    'rgba(255, 159, 64, 1)',
    'rgba(255, 159, 65, 1)'
  ];

  constructor(
    private router: Router,
    private api: ApiIntractionsService,
    private capacityUtilizationService: CapacityUtilizationService,
    private projectService: ProjectService
  ) {
    let currentStateExtras = this.router.getCurrentNavigation()?.extras.state;
    this.projectId = currentStateExtras;
  }

  ngOnInit(): void {
    if (this.projectId) {
      this.getProjectDetail();
      this.getGanttChartData();
      this.getBarChartData();
    }
    this.getPieChartData();
  }


  getProjectDetail() {
    this.projectService.getById(this.projectId.projectId).subscribe((res) => {
      this.projectStartDate = this.formatDate(res.effectiveStartDate);
      this.ganttChartOptions = {
      scales: {
        x: {
          position: 'top',
          min: this.projectStartDate,
          type: 'time',
          time: {
            unit: 'week'
          }
        },
        y: { beginAtZero: true }
      }
    }
    });
  }

   formatDate(date:any) {
  var d = new Date(date),
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

  if (month.length < 2)
    month = '0' + month;
  if (day.length < 2)
    day = '0' + day;

  return [year, month, day].join('-');
}

  getGanttChartData() {
    this.projectService.getganttChart(this.projectId.projectId).subscribe((res) => {
      this.ganttChartList = res;
      console.log('this.ganttChartList ', this.ganttChartList)
      if (this.ganttChartList.length > 0) {
        for (var i = 0; i < this.ganttChartList.length; i++) {
          this.ganttXaxis.push(this.ganttChartList[i].name);
          this.ganttYaxis.push(this.ganttChartList[i].startDate, this.ganttChartList[i].endDate);
          let selectColor = Math.floor((Math.random() * 6) + 1);
          this.ganttcolors.push(this.setColors[selectColor]);
          this.ganttChartData = {
            labels: this.ganttXaxis,
            datasets: [{
              label: 'Hours/day',
              data: this.ganttYaxis,
              backgroundColor: this.ganttcolors,
              barPercentage: 0.3,
            }],
          }
        };
      }
    });
  }

  getPieChartData() {
    this.projectService.getAllPieChart().subscribe((res) => {
      this.pieChartList = res;
     
      if (this.pieChartList.length > 0) {
        for (var i = 0; i < this.pieChartList.length; i++) {
          this.pieXaxis.push(this.pieChartList[i].hours);
          this.pieYaxis.push(this.pieChartList[i].name);
          this.pieChartLabels = this.pieYaxis;
          this.pieChartDatasets = [{ data: this.pieXaxis }];
        };
      }
    });
  }

  getBarChartData() {
    if (this.projectId) {
      this.capacityUtilizationService.getAll(this.projectId.projectId)
        .subscribe((res) => {
          this.capacityList = res;
          if (this.capacityList.length > 0) {
            for (var i = 0; i < this.capacityList.length; i++) {
              this.capacityXaxis.push(this.capacityList[i].hours);
              this.capacityYaxis.push(this.capacityList[i].role.code);
              this.barChartData = {
                labels: this.capacityYaxis,
                datasets: [{ data: this.capacityXaxis, label: 'Hours/day' }],
              };
            }
          }
        });
    }
  }

  

  @Input()
  chartType!: string;  //To define which chart to be used

  // Pie Chart
  public pieChartOptions: ChartOptions<'pie'> = {
    responsive: false,
    plugins: {
      legend: { position: 'right' }
    },
  };
  public pieChartLabels = [''];
  public pieChartDatasets = [ {data: [0 ]} ];
  public pieChartLegend = true;
  public pieChartPlugins = [];
  
  // Bar Chart
  public barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: { y: { beginAtZero: true } },
    indexAxis: 'y',
  };
  public barChartLabels: string[] = [''];
  public barChartType: ChartType = 'bar';
  public barChartLegend: boolean = true;

  public barChartData:  ChartData<'bar'>  = {
    labels: [''],
    datasets: [{ data: [0], label: 'Hours/day' }],
  };

  //Gantt Chart
  public ganttChartData : ChartData<'bar'>  = {
    labels: [''],
    datasets: [{ 
      label: 'Hours/day',
      data: [   ],  
      backgroundColor: [
        'rgba(255, 26, 104, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
        'rgba(0, 0, 0, 1)'
      ],
        barPercentage:0.3,
    }],  
  }
  public ganttChartOptions: ChartConfiguration['options'] = {
    indexAxis: 'y',
    scales: { 
      x:{
        position:'top',
        min: '2023-10-11',
        type:'time',
        time: {
          unit:'week'
        }
      },
      y: { beginAtZero: true } 
    },
    plugins: {
      tooltip: {
        callbacks: {
          title: (context :any) => {
            const startDate = new Date(context[0].raw[0]).toLocaleString([],{
              year:'numeric',
              month:'short',
              day:'numeric'
            });
            const endDate = new Date(context[0].raw[1]).toLocaleString([],{
              year:'numeric',
              month:'short',
              day:'numeric'
            });
            return startDate + ' to ' + endDate;
          }
        }
      }
    },
  }
    
  public ganttChartLabels: string[] = [''];
  public ganttChartType: ChartType = 'bar';
  public ganttChartLegend: boolean = false;
}


