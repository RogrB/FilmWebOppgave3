import { Component, OnInit } from "@angular/core";

@Component({
    selector: "app-footer",
    templateUrl: "./footer.html",
    styleUrls: ['./footer.css']
})

export class footer {

    get dato() {
        return new Date();
    }

}


