import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { VehicleUpdateRequest } from './models/vehicle-update-request';
import { Observable } from 'rxjs';
import { Vehicle } from './models/vehicle';
import { VehicleCreateRequest } from './models/vehicle-create-request';

@Injectable({
  providedIn: 'root'
})
export class VehicleServiceService {

  baseUrl = "http://localhost:5166/api/vehicles";

  constructor(private httpClient: HttpClient) { }

  private getUrl() {
    return `${this.baseUrl}`;
  }

  public getVehicles() : Observable<any> {
    const url = this.getUrl() + "/vehicles";
    return this.httpClient.get(url);
  }

  getVehicleById(VehicleId: string): Observable<Vehicle> {
    const url = this.getUrl() + "/vehicles"+ `/${VehicleId}`;
    return this.httpClient.get<Vehicle>(url);
  }

  createVehicle(Vehicle: VehicleCreateRequest): Observable<VehicleCreateRequest> {
    const url = this.getUrl() + "/create";
    return this.httpClient.post<VehicleCreateRequest>(url, Vehicle);
  }

  updateVehicle(Vehicle: VehicleUpdateRequest): Observable<VehicleUpdateRequest> {
    const url = this.getUrl() + "/update";
    return this.httpClient.put<VehicleUpdateRequest>(url, Vehicle);
  }

  deleteVehicle(vehicleId: string): Observable<any> {
    const url = this.getUrl() + "/delete"+ `/${vehicleId}`;
    return this.httpClient.delete(url);
  }
}
