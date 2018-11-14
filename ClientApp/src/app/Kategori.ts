import { Sp } from "./sp";

export class Kategori {
    id: number;
    navn: string;
    sp: Array<Sp>;
    underkategorier: Array<UnderKategori>;
}

export class UnderKategori {
    id: number;
    navn: string;
    sp: Array<Sp>;
}
