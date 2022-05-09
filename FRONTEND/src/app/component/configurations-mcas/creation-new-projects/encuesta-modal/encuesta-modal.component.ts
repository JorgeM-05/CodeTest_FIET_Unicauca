import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PollServiceService } from 'src/app/service/poll-service.service';

@Component({
  selector: 'app-encuesta-modal',
  templateUrl: './encuesta-modal.component.html',
  styleUrls: ['./encuesta-modal.component.css']
})
export class EncuestaModalComponent implements OnInit {  
  
  myForm: FormGroup;

  constructor(
    private _pollService: PollServiceService,
    private snackBar: MatSnackBar,
    private myFormBuilder: FormBuilder,
    public dialogRef: MatDialogRef<EncuestaModalComponent>,
    @Inject(MAT_DIALOG_DATA) public message: string) {   
      
      this.myForm = this.myFormBuilder.group({
        question_1_1: ['',[]],
        question_1_2: [''],
        question_1_3: [''],
        question_1_4: [''],
        question_2_1: [''],
        question_2_2: [''],
        question_2_3: [''],
        question_2_4: [''],
        question_3_1: [''],
        question_3_2: [''],
        question_3_3: [''],
        question_3_4: [''],
        question_4: [''],
        question_5: [''],
        question_6: [''],
        question_7: [''],
        question_8: [''],
        question_9: [''],
        question_10: ['']    
      });
      }  

  ngOnInit(): void {
  }

  onClickNo():void {
    this.dialogRef.close();
  }

  savePoll() {
    const POLL = {
      q_1: {
        q_1_1: this.myForm.get('question_1_1').value,
        q_1_2: this.myForm.get('question_1_2').value,
        q_1_3: this.myForm.get('question_1_3').value,
        q_1_4: this.myForm.get('question_1_4').value,        
      },
      q_2: {
        q_2_1: this.myForm.get('question_2_1').value,
        q_2_2: this.myForm.get('question_2_2').value,
        q_2_3: this.myForm.get('question_2_3').value,
        q_2_4: this.myForm.get('question_2_4').value,        
      },
      q_3: {
        q_3_1: this.myForm.get('question_3_1').value,
        q_3_2: this.myForm.get('question_3_2').value,
        q_3_3: this.myForm.get('question_3_3').value,
        q_3_4: this.myForm.get('question_3_4').value,        
      },
      q_4: this.myForm.get('question_4').value,
      q_5: this.myForm.get('question_5').value,
      q_6: this.myForm.get('question_6').value,
      q_7: this.myForm.get('question_7').value,
      q_8: this.myForm.get('question_8').value,
      q_9: this.myForm.get('question_9').value,
      q_10: this.myForm.get('question_10').value,
    };

    this._pollService.addPoll(POLL).subscribe(datos => {     
        });             
  }
}
