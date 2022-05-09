export interface DataProjectI{
    PropertiesProject:{
        _idList:string,
        _iduser:string,
        nombre:string,
        company:string,
        fecha:string
    },
    DataProject:{
        _idProject:string,
        metodo:string,
        nameClass:string
        strength:string,
        tipo_de_metodo:string
    },
    variables:Array<variables>
}

export interface variables{
    _idVariable:string,
    _idProject:string,
    nombre_var:string,
    tipo_de_variable:string,
    valores:string
}