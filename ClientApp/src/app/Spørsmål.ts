import { Kategori } from "./Kategori";

export class Spørsmål {
    id: number;
    spørsmål: string;
    poeng: number;
    antallStemmer: number;
    svar: Array<Svar>;
}

export class Svar {
    id: number;
    svar: string;
    poeng: number;
    antallStemmer: number;
}
