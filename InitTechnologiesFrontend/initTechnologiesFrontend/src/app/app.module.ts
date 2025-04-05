import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { JwtModule } from '@auth0/angular-jwt';
import { VehicleComponent } from './vehicle/vehicle.component';
import { VehicleCreateDialogComponent } from './vehicle-create-dialog/vehicle-create-dialog.component';
import { VehicleDeleteDialogComponent } from './vehicle-delete-dialog/vehicle-delete-dialog.component';
import { VehicleUpdateDialogComponent } from './vehicle-update-dialog/vehicle-update-dialog.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';


export function tokenGetter() {
  return localStorage.getItem("jwt");
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    VehicleComponent,
    VehicleCreateDialogComponent,
    VehicleDeleteDialogComponent,
    VehicleUpdateDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        allowedDomains: ["localhost:5166"],
        disallowedRoutes: []
      } as any
    }),
    BrowserAnimationsModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
