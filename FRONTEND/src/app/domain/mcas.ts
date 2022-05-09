export interface mcasI {
    name: string,
    static: string,
    return: string,
    strength: string,
    NumVar: string,
    var: string,

}
export interface parametersArrayI {
    NameVariable: string,
    value: number,
    Type: number
}

export interface mcasE {
    columns: number,
    strength: number,
    alphabet: string,
}
export interface variablesReq {
    name_var: string,
    type_var: string
}
export interface index {
    i: number;
}

export interface mcasIE {
    Alphabet: string,
    Columns: number,
    Strength: number,

}
export interface ResponseMcasI{
    //_idProject:any,
    //_idUser:any,
    caid: any,
    columns: any,
    strength: any,
    alphabet: any,
    rows: any,
    cA_notes: any,
    aux: any,
    code: any
}
