"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var footer = /** @class */ (function () {
    function footer() {
    }
    Object.defineProperty(footer.prototype, "dato", {
        get: function () {
            return new Date();
        },
        enumerable: true,
        configurable: true
    });
    footer = __decorate([
        core_1.Component({
            selector: "app-footer",
            templateUrl: "./app/footer.html",
            styleUrls: ['./app/footer.css']
        })
    ], footer);
    return footer;
}());
exports.footer = footer;
//# sourceMappingURL=footer.js.map