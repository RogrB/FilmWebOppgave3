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
    if (streng != null && streng != "" && streng != undefined) {
      this._http.get("api/FAQ/HentForslag/" + streng)
        .subscribe(
          returData => {
            let resultat = returData.json();
            if (resultat) {
              this.visForslagsListe = true;
              this.alleForslag = [];
              for (let objekt of resultat) {
                let nyttForslag = new Forslag();
                nyttForslag.id = objekt.id;
                nyttForslag.sp = this.markering(objekt.sp);
                this.alleForslag.push(nyttForslag);
              }
            }
            else {
              console.log("Klarte ikke å hente søkeforslag");
              this.visForslagsListe = false;
            }
          },
          error => alert(error)
      );
    }
    else {
      this.visForslagsListe = false;
    }
  }

  public markering(innhold) {
    let sokeTekst = this.forslag.value.sok;
    if (!sokeTekst) {
      return innhold;
    }
    return innhold.replace(new RegExp(sokeTekst, "gi"), match => {
      return '<span class="markering">' + match + '</span>';
    });
  }

  sendTilComponent(id) {
    this.visForslagsListe = false;
    this.hentSp.emit(id);
  }

}
