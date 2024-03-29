import { Component, OnInit, Input, ChangeDetectorRef } from "@angular/core";
import { Http, Response, } from '@angular/http';
import { Headers } from '@angular/http';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';

// RXJS
import "rxjs/add/operator/map";

// Custom
import { Sp } from "./sp";
import { Svar } from "./sp";
import { Kategori } from "./Kategori";

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
  topListe: Array<Sp>;
  alleKategorier: Array<Kategori>;
  kategoriSkjema: FormGroup;
  spSkjema: FormGroup;
  svarSkjema: FormGroup;
  enKategori: Kategori;
  ettSp: Sp;
  @Input() forslag: Input;

  laster: boolean;

  constructor(private _http: Http, private fb: FormBuilder, private cd: ChangeDetectorRef) {

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
    this.hentTopListe();
    this.visKategoriSkjema = false;
    this.visKategorier = true;
    this.visspSkjema = false;
    this.vissp = false;
  }

  // Viser registreringskjema for ny kategori
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

  // Sender registrering av kategori til API
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
            }
            this.laster = false;
            this.cd.detectChanges();
          };
        },
        error => alert(error),
        () => console.log("ferdig get-api/kategorier")
      );
  };

  visKategoriView(id: number) {
    this.laster = true;
    this.hentKategori(id);
  }

  tilbakeTilKategoriView() {
    this.vissp = false;
    this.visKategori = true;
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
    this.hentAlleKategorier();
    this.hentTopListe();
    this.visKategorier = true;
  }
  
  // Skriver spørsmål til db
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
    console.log(id);
    this.visKategori = false;
    this.visKategorier = false;
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
        error => alert(error)
      );
  }

  stemSpOpp(id: number) {
    this._http.get("api/Stemme/SPOpp/" + id)
      .subscribe(
        returData => {
          let resultat = returData.json();
          if (resultat) {
            this.visSpView(this.ettSp.id);
          }
          else {
            console.log("Klarte ikke å stemme");
          }
        },
        error => alert(error)
      );
  }

  stemSpNed(id: number) {
    this._http.get("api/Stemme/SPNed/" + id)
      .subscribe(
        returData => {
          let resultat = returData.json();
          if (resultat) {
            this.visSpView(this.ettSp.id);
          }
          else {
            console.log("Klarte ikke å stemme");
          }
        },
        error => alert(error)
      );
  }

  stemSvarNed(id: number) {
    this._http.get("api/Stemme/SvarNed/" + id)
      .subscribe(
        returData => {
          let resultat = returData.json();
          if (resultat) {
            this.visSpView(this.ettSp.id);
          }
          else {
            console.log("Klarte ikke å stemme");
          }
        },
        error => alert(error)
      );
  }

  stemSvarOpp(id: number) {
    this._http.get("api/Stemme/SvarOpp/" + id)
      .subscribe(
        returData => {
          let resultat = returData.json();
          if (resultat) {
            this.visSpView(this.ettSp.id);
          }
          else {
            console.log("Klarte ikke å stemme");
          }
        },
        error => alert(error)
      );
  }

  // Henter de 5 øverste spørsmålene basert på poeng
  hentTopListe() {
    this._http.get("api/FAQ/HentTopListe/")
      .subscribe(
        returData => {
          let resultat = returData.json();
          if (resultat) {
            this.topListe = [];
            for (let objekt of resultat) {
              let nyttSp = new Sp();
              nyttSp.sp = objekt.sp;
              nyttSp.poeng = objekt.poeng;
              nyttSp.id = objekt.id;
              this.topListe.push(nyttSp);
            }
            this.cd.detectChanges();
          }
          else {
            console.log("Klarte ikke å hente topliste av spørsmål");
          }
        },
        error => alert(error)
      );
  }
}
