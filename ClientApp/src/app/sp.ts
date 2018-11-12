import { Kategori } from "./Kategori";

export class Sp {
    id: number;
    sp: string;
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
