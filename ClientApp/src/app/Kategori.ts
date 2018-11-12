import { sp } from "./sp";

export class Kategori {
    id: number;
    navn: string;
    sp: Array<sp>;
    underkategorier: Array<UnderKategori>;
}

export class UnderKategori {
    id: number;
    navn: string;
    sp: Array<sp>;
}
