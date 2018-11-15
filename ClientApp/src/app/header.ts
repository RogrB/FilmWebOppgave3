import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { FormGroup, FormControl, Validators, FormBuilder, Form } from '@angular/forms';
import { Http, Response, } from '@angular/http';
import { Forslag } from "./sp";

@Component({
    selector: "app-header",
    templateUrl: "./header.html",
    styleUrls: ['./header.css']

})
export class header {
  forslag: FormGroup;
  alleForslag: Array<Forslag>;
  visForslagsListe: boolean;

  @Output() hentSp = new EventEmitter<number>();

  constructor(private _http: Http, private fb: FormBuilder) {
    this.forslag = fb.group({
      id: [""],
      sok: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ?]{1,100}")])]
    });
  }

  nginit() {
    this.visForslagsListe = false;
  }

  hentForslag() {
    var streng = this.forslag.value.sok;
       console.log(streng);
    if (streng != null && streng != "" && streng != undefined) {
      this._http.get("api/FAQ/HentForslag/" + streng)
      
        .subscribe(
          returData => {
            let resultat = returData.json();
            if (resultat) {
              this.visForslagsListe = true;
              this.alleForslag = [];
              for (let objekt of resultat) {
                this.alleForslag.push(objekt);
                console.log(objekt);
              }
            }
            else {
              console.log("Klarte ikke å hente søkeforslag");
              this.visForslagsListe = false;
            }
          },
          error => alert(error),
      () => console.log("ferdig get-api/HentForslag")
      );
    }
    else {
      this.visForslagsListe = false;
    }
  }

  sendTilComponent(id) {
    this.visForslagsListe = false;
    this.hentSp.emit(id);
  }

}
