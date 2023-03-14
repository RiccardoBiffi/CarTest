export interface AvailableCar {
  Id: number;
  CategoryId: number;
  Make: string;
  Model: string;
  AirConditioning: boolean;
  Doors: number;
  Passengers: number;
  IsAutomatic: boolean;
  Picture: string;
  CategoryName: string;
  DailyRate: number;
}

export interface Car {
  Id: number;
  CategoryId: number;
  Make: string;
  Model: string;
  Picture: string;
  AirConditioning: boolean;
  Passengers: number;
  Doors: number;
  IsAutomatic: boolean;
}

export interface Category {
  Id: number;
  Name: string;
  DailyRate: number;
}

export interface Order {
  Id: number;
  OrderDate: string;
  StartDate: string;
  EndDate: string;
  Description: string;
  CarId: number;
}
