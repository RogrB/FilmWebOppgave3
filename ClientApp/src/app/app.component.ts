import { Component, OnInit } from "@angular/core";
import { Http, Response, } from '@angular/http';
import { Headers } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

// RXJS
import "rxjs/add/operator/map";

// Custom
import { Spørsmål } from "./Spørsmål";
import { Kategori } from "./Kategori";
import { UnderKategori } from "./Kategori";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'app';
  visKategoriSkjema: boolean;
  visSpørsmålSkjema: boolean
  visSpørsmål: boolean;
  visKategorier: boolean;
  alleSpørsmål: Array<Spørsmål>;
  alleKategorier: Array<Kategori>;
  kategoriSkjema: FormGroup;
  spørsmålSkjema: FormGroup;

  // Gamle
  visSkjema: boolean;
  skjemaStatus: string;
  visKundeListe: boolean;
  //alleKunder: Array<Kunde>; // for listen av alle kundene
  skjema: FormGroup;

  laster: boolean; // behold denne

  constructor(private _http: Http, private fb: FormBuilder) {

    this.kategoriSkjema = fb.group({
      id: [""],
      navn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
    });

    this.spørsmålSkjema = fb.group({
      id: [""],
      spørsmål: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
    });

    // Gammal
    this.skjema = fb.group({
      id: [""],
      fornavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
      etternavn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
      adresse: [null, Validators.compose([Validators.required, Validators.pattern("[0-9a-zA-ZøæåØÆÅ\\-. ]{2,30}")])],
      postnr: [null, Validators.compose([Validators.required, Validators.pattern("[0-9]{4}")])],
      poststed: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
    });
  }

  ngOnInit() {
    this.laster = true;

    this.hentAlleKategorier();
    this.visSkjema = false;
    this.visKundeListe = false;
    this.visKategoriSkjema = false;
    this.visKategorier = true;
    this.visSpørsmålSkjema = false;
    this.visSpørsmål = false;
  }

  kategoriSkjemaView() {
    this.kategoriSkjema.setValue({
      id: "",
      navn: ""
    });
    this.kategoriSkjema.markAsPristine();
    this.visKategorier = false;
    this.visKategoriSkjema = true;
  }

  registrerKategori() {
    var nyKategori = new Kategori();

    nyKategori.navn = this.kategoriSkjema.value.navn;

    var body: string = JSON.stringify(nyKategori);
    var headers = new Headers({ "Content-Type": "application/json" });

    this._http.post("api/Kategori/", body, { headers: headers })
      .map(returData => returData.toString())
      .subscribe(
        retur => {
          this.hentAlleKategorier();
          this.visSkjema = false;
          this.visKundeListe = false;
          this.visKategoriSkjema = false;
          this.visKategorier = true;
        },
        error => alert(error),
        () => console.log("ferdig post-api/FAQ")
      );
  }

  hentAlleKategorier() {
    this._http.get("api/Kategori/")
      .map(returData => {
        let JsonData = returData.json();
        return JsonData;
      })
      .subscribe(
        JsonData => {
          this.alleKategorier = [];
          if (JsonData) {
            for (let kategoriObjekt of JsonData) {
              this.alleKategorier.push(kategoriObjekt);
              this.laster = false;
            }
          };
        },
        error => alert(error),
        () => console.log("ferdig get-api/kategori")
      );
  };

  tilKategoriListe() {
    this.visKategoriSkjema = false;
    this.visKategorier = true;
  }

  /*
  hentAlleKunder() {
    this._http.get("api/kunde/")
      .map(returData => {
        console.log(returData, 'hentAlleKunder');
        let JsonData = returData.json();
        return JsonData;
      })
      .subscribe(
        JsonData => {
          this.alleKunder = [];
          if (JsonData) {
            for (let kundeObjekt of JsonData) {
              this.alleKunder.push(kundeObjekt);
              this.laster = false;
            }
          };
        },
        error => alert(error),
        () => console.log("ferdig get-api/kunde")
      );
  };

  vedSubmit() {
    if (this.skjemaStatus == "Registrere") {
      this.lagreKunde();
    }
    else if (this.skjemaStatus == "Endre") {
      this.endreEnKunde();
    }
    else {
      alert("Feil i applikasjonen!");
    }
  }

  registrerKunde() {
    // må resette verdiene i skjema dersom skjema har blitt brukt til endringer

    this.skjema.setValue({
      id: "",
      fornavn: "",
      etternavn: "",
      adresse: "",
      postnr: "",
      poststed: ""
    });
    this.skjema.markAsPristine();
    this.visKundeListe = false;
    this.skjemaStatus = "Registrere";
    this.visSkjema = true;
  }

  tilbakeTilListe() {
    this.visKundeListe = true;
    this.visSkjema = false;
  }

  lagreKunde() {
    var lagretKunde = new Kunde();

    lagretKunde.fornavn = this.skjema.value.fornavn;
    lagretKunde.etternavn = this.skjema.value.etternavn;
    lagretKunde.adresse = this.skjema.value.adresse;
    lagretKunde.postnr = this.skjema.value.postnr;
    lagretKunde.poststed = this.skjema.value.poststed;

    var body: string = JSON.stringify(lagretKunde);
    var headers = new Headers({ "Content-Type": "application/json" });

    this._http.post("api/kunde", body, { headers: headers })
      .map(returData => returData.toString())
      .subscribe(
        retur => {
          this.hentAlleKunder();
          this.visSkjema = false;
          this.visKundeListe = true;
        },
        error => alert(error),
        () => console.log("ferdig post-api/kunde")
      );
  };

  sletteKunde(id: number) {
    this._http.delete("api/kunde/" + id)
      .map(returData => returData.toString())
      .subscribe(
        retur => {
          this.hentAlleKunder();
        },
        error => alert(error),
        () => console.log("ferdig delete-api/kunde")
      );
  };
  // her blir kunden hentet og vist i skjema
  endreKunde(id: number) {
    this._http.get("api/kunde/" + id)
      .map(returData => {
        let JsonData = returData.json();
        return JsonData;
      })
      .subscribe(
        JsonData => { // legg de hentede data inn i feltene til endreSkjema. Kan bruke setValue også her da hele skjemaet skal oppdateres. 
          this.skjema.patchValue({ id: JsonData.id });
          this.skjema.patchValue({ fornavn: JsonData.fornavn });
          this.skjema.patchValue({ etternavn: JsonData.etternavn });
          this.skjema.patchValue({ adresse: JsonData.adresse });
          this.skjema.patchValue({ postnr: JsonData.postnr });
          this.skjema.patchValue({ poststed: JsonData.poststed });
        },
        error => alert(error),
        () => console.log("ferdig get-api/kunde")
      );
    this.skjemaStatus = "Endre";
    this.visSkjema = true;
    this.visKundeListe = false;
  }
  // her blir den endrede kunden lagret
  endreEnKunde() {
    var endretKunde = new Kunde();

    endretKunde.fornavn = this.skjema.value.fornavn;
    endretKunde.etternavn = this.skjema.value.etternavn;
    endretKunde.adresse = this.skjema.value.adresse;
    endretKunde.postnr = this.skjema.value.postnr;
    endretKunde.poststed = this.skjema.value.poststed;

    var body: string = JSON.stringify(endretKunde);
    var headers = new Headers({ "Content-Type": "application/json" });

    this._http.put("api/kunde/" + this.skjema.value.id, body, { headers: headers })
      .map(returData => returData.toString())
      .subscribe(
        retur => {
          this.hentAlleKunder();
          this.visSkjema = false;
          this.visKundeListe = true;
        },
        error => alert(error),
        () => console.log("ferdig post-api/kunde")
      );
  }*/

}
