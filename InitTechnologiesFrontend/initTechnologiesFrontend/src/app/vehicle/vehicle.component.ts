import { Component } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { Router } from '@angular/router';
import { VehicleCreateDialogComponent } from '../vehicle-create-dialog/vehicle-create-dialog.component';
import { VehicleCreateRequest } from '../models/vehicle-create-request';
import { VehicleUpdateDialogComponent } from '../vehicle-update-dialog/vehicle-update-dialog.component';
import { VehicleUpdateRequest } from '../models/vehicle-update-request';
import { VehicleDeleteDialogComponent } from '../vehicle-delete-dialog/vehicle-delete-dialog.component';
import { VehicleServiceService } from '../vehicle-service.service';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.css']
})
export class VehicleComponent {
  vehicles: Vehicle[] = [];

  searchQuery: string = '';

  sortedField: string = ''; 
  sortDirection: number = 1; 

  helperFlag: boolean = false;

  pageSize = 3;
  currentPage = 0;
  totalPages = 0;
  paginatedVehicles: Vehicle[] = [];
  pageNumbers: number[] = [];
  filteredVehicles: Vehicle[] = [];
  sortOrder: { [key: string]: boolean } = {
    vehicleId: true,
    vehicleName: true,
    vehicleYear: true,
    vehicleFuelType: true
  };
  currentSortKey: string = '';
  currentSortDirection: boolean = true;

  constructor(private vehicleService: VehicleServiceService, 
    private dialogModel: MatDialog,
    private router: Router) { }

  ngOnInit(): void {
    this.startSubscription();
  }

  startSubscription() {
    this.vehicleService.getVehicles().subscribe((res: Vehicle[]) => {
      this.vehicles = res;
    });
  }

  openDialogForCreate(): void {
    const dialog = this.dialogModel.open(VehicleCreateDialogComponent, { 
      width: '600px',
      data: {vehicleName: "", vehicleYear: "", vehicleFuelType: ""}
    });

    dialog.afterClosed().subscribe((result: { vehicleName: any; vehicleYear: any; vehicleFuelType: any; }) => {
      if (result) {
        const vehicle : VehicleCreateRequest = {
          vehicleName: result.vehicleName,
          vehicleYear: result.vehicleYear,
          vehicleFuelType: result.vehicleFuelType
        }
        this.createVehicle(vehicle);
      }
    });
  }

  private createVehicle(vehicle: VehicleCreateRequest) {
    this.vehicleService.createVehicle(vehicle).subscribe(
      (      response: any) => {
        this.loadData();
        setTimeout(() => {
          window.location.reload();
        }, 700);
       
      },
      (      error: any) => {
        console.error('failed', error);
       
      }
    );
  }

  openDialogForUpdate(vehicle : Vehicle): void {
    const dialog = this.dialogModel.open(VehicleUpdateDialogComponent, { 
      width: '600px',
      data : {
        vehicleId: vehicle.vehicleId,
        vehicleName: vehicle.name,
        vehicleYear: vehicle.year,
        vehicleFuelType: vehicle.fuelType
      }
    });
    dialog.afterClosed().subscribe( (result: { vehicleName: any; vehicleYear: any; vehicleFuelType: any; }) => {
      if (result) { 
        const vehicle : VehicleUpdateRequest = {
          vehicleName: result.vehicleName,
          vehicleYear: result.vehicleYear,
          vehicleFuelType: result.vehicleFuelType
        }
        this.updateVehicle(vehicle);
      }
    });
  }

  updateVehicle(updatedVehicle: VehicleUpdateRequest): void {
    this.vehicleService.updateVehicle(updatedVehicle).subscribe(
      (      updatedVehicle: any) => {
        this.loadData();
        setTimeout(() => {
          window.location.reload();
        }, 700);
      },
      (      error: any) => {
        console.error('Failed', error);
      }
    );
  }

  openDialogForDelete(vehicle: Vehicle): void {
    const dialog = this.dialogModel.open(VehicleDeleteDialogComponent, {
        width: '400px',
        data: {
          vehicleId: vehicle.vehicleId,
          vehicleName: vehicle.name,
          vehicleYear: vehicle.year,
          vehicleFuelType: vehicle.fuelType
        } 
    });
    dialog.afterClosed().subscribe((result: { vehicleId: string; }) => {
        if (result) {
            this.deleteVehicle(result.vehicleId);
        }
    });
}

  deleteVehicle(vehicleId: string): void {
  this.vehicleService.deleteVehicle(vehicleId).subscribe(
    (      response: any) => {
          this.loadData();
          setTimeout(() => {
            window.location.reload();
          }, 700);
      },
    (      error: any) => {
          console.error(`failed`, error);
      }
  );
}


  loadData(): void {
    this.vehicleService.getVehicles().subscribe(
      (      data: any) => {
        this.vehicles = data;
      },
      (      error: any) => {
        console.error('Greška pri učitavanju podataka:', error);
      }
    );
  }
  

}
