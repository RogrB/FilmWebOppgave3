import { Component, OnInit } from "@angular/core";

@Component({
    selector: "app-footer",
    templateUrl: "./footer.html",
    styleUrls: ['./footer.css']
})

export class footer {
  dagensDato: Date;

  // Eksporterer dagens dato til footeren
  constructor() {
    this.dagensDato = new Date();
  }

}


