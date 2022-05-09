import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn:'root'
})

export class callcomponents{

    invokeEvent:Subject<any>=new Subject()
    invokeTaskCancell:Subject<any>=new Subject()
    invokeTaskUpdate:Subject<any>=new Subject()
    invokeTaskChangeForm:Subject<any>=new Subject()

    constructor(){}

    callsecondcomponent(value:any){
        this.invokeEvent.next(value)
    }

}