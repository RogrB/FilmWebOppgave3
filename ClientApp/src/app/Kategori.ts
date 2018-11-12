import { Spørsmål } from "./Spørsmål";

export class Kategori {
    id: number;
    navn: string;
    spørsmål: Array<Spørsmål>;
    underkategorier: Array<UnderKategori>;
}

export class UnderKategori {
    id: number;
    navn: string;
    spørsmål: Array<Spørsmål>;
}
