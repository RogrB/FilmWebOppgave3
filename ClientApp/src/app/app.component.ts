import { Component, OnInit } from "@angular/core";
import { Http, Response, } from '@angular/http';
import { Headers } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

// RXJS
import "rxjs/add/operator/map";

// Custom
import { Sp } from "./sp";
import { Svar } from "./sp";
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
  visspSkjema: boolean
  vissp: boolean;
  visKategorier: boolean;
  visKategori: boolean;
  allesp: Array<Sp>;
  alleKategorier: Array<Kategori>;
  kategoriSkjema: FormGroup;
  spSkjema: FormGroup;
  svarSkjema: FormGroup;
  enKategori: Kategori;
  ettSp: Sp;

  laster: boolean;

  constructor(private _http: Http, private fb: FormBuilder) {

    this.kategoriSkjema = fb.group({
      id: [""],
      navn: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ]{2,30}")])]
    });

    this.spSkjema = fb.group({
      id: [""],
      sp: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ?]{1,100}")])]
    });

    this.svarSkjema = fb.group({
      id: [""],
      svar: [null, Validators.compose([Validators.required, Validators.pattern("[a-zA-ZøæåØÆÅ\\-. ?]{1,100}")])]
    });

  }

  ngOnInit() {
    this.laster = true;

    this.hentAlleKategorier();
    this.visKategoriSkjema = false;
    this.visKategorier = true;
    this.visspSkjema = false;
    this.vissp = false;
  }

  kategoriSkjemaView() {
    this.kategoriSkjema.setValue({
      id: "",
      navn: ""
    });
    this.kategoriSkjema.markAsPristine();
    this.visKategorier = false;
    this.vissp = false;
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

  visKategoriView(id: number) {
    this.laster = true;
    this.hentKategori(id);
  }

  hentKategori(id: number) {
    this._http.get("api/Kategori/" + id)
      .subscribe(
        returData => { 
          let objekt = returData.json();
          this.enKategori = new Kategori();
          this.enKategori.id = objekt.id;
          this.enKategori.navn = objekt.navn;
          if (objekt.sp != null) {
            this.enKategori.sp = objekt.sp;
          }
          if (objekt.underkategorier != null) {
            this.enKategori.underkategorier = objekt.underkategorier;
          }

          this.laster = false;
          this.visKategori = true;
          this.visKategorier = false;
        },
        error => alert(error),
        () => console.log("ferdig get-api/Kategori")
      );
      
  }

  tilKategoriListe() {
    this.visKategoriSkjema = false;
    this.visKategori = false;
    this.vissp = false;
    this.visKategorier = true;
  }
  
  
  skrivSP(id: number) {
    var nyttSp = new Sp();
    nyttSp.sp = this.spSkjema.value.sp;

    var body: string = JSON.stringify(nyttSp);
    var headers = new Headers({ "Content-Type": "application/json" });

    this._http.post("api/FAQ/" + id, body, { headers: headers })
      .map(returData => returData.toString())
      .subscribe(
      retur => {
        this.spSkjema.setValue({
            id: "",
            sp: ""
          });
          this.laster = false;
          this.visKategori = true;
          this.visKategorier = false;
          this.hentKategori(this.enKategori.id);
        },
        error => alert(error),
        () => console.log("ferdig post-api/FAQ")
      );
  }
  
  visSpView(id: number) {
    this.visKategori = false;
    this.hentSp(id);
  }

  hentSp(id: number) {
    this._http.get("api/FAQ/" + id)
      .subscribe(
        returData => {
          let objekt = returData.json();
          this.ettSp = new Sp();
          this.ettSp.id = objekt.id;
          this.ettSp.sp = objekt.sp;
          this.ettSp.poeng = objekt.poeng;
          this.ettSp.antallStemmer = objekt.antallStemmer;
          if (objekt.svar != null) {
            this.ettSp.svar = objekt.svar;
          }

          this.laster = false;
          this.visKategori = false;
          this.vissp = true;
        },
        error => alert(error),
        () => console.log("ferdig get-api/Kategori")
      );
  }

  skrivSvar(id: number) {
    var nyttSvar = new Svar();
    nyttSvar.svar = this.svarSkjema.value.svar;

    var body: string = JSON.stringify(nyttSvar);
    var headers = new Headers({ "Content-Type": "application/json" });

    this._http.post("api/Svar/" + id, body, { headers: headers })
      .map(returData => returData.toString())
      .subscribe(
        retur => {
          this.svarSkjema.setValue({
            id: "",
            svar: ""
          });
          this.laster = false;
          this.visKategori = false;
          this.visKategorier = false;
          this.vissp = true;
          this.visSpView(this.ettSp.id);
        },
        error => alert(error),
        () => console.log("ferdig post-api/Svar")
      );
  }

  stemSpOpp(id: number) {

  }

  stemSpNed(id: number) {

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
