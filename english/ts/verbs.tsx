
interface AppVerbProps {
    present: string,
    past: string,
    perfect: string,
    callback?: (answer:any) => (void)
}

class AppVerb extends React.Component<AppVerbProps,{}> {

   private p1: any;
   private p2: any;
   private p3: any;

   answer() {
       var ans = {
            present: this.p1.userAnswer(),
            past: this.p2.userAnswer(),
            perfect: this.p3.userAnswer()
       }

       if(this.props.callback) {
            this.props.callback(ans);
       } else {
            alert(JSON.stringify(ans));
       }
        
   }

   render() {
      return (
         <div>
            <form autoFocus>
             <div className="verbtable col-md-8 col-md-offset-2">
                <div className="row">
                    <span className="col-md-4"><VerbText ref={c=> this.p1=c}  label="Present"           answer={this.props.present}/></span>
                    <span className="col-md-4"><VerbText ref={c=> this.p2=c}  label="Past"              answer={this.props.past} /></span>
                    <span className="col-md-4"><VerbText ref={c=> this.p3=c}  label="Present Perfect"   answer={this.props.perfect}/></span>
                </div>                
            </div>

            <div className="verbsubmit col-md-12">
            <button type="submit" className="btn btn-lg btn-primary" onClick={this.answer.bind(this)}>Submit</button>
                
            </div>
           </form>
         </div>
      );
   }
}

interface VerbTextProps {
    answer: string | null;
    label: string;
}

class VerbText extends React.Component<VerbTextProps,{}> {
    
    private input: any;

    userAnswer() {
        return (this.props.answer != null) ? this.props.answer : this.input.value;
    }

    render() {

        let label = this.props.label;
        let verb = this.props.answer;

        if(verb != null) {
            return <p className="form-control-static"><strong>{ verb }</strong></p>;            
        }

        return <input className="form-control text-center" type="text" placeholder={label} ref={input=>this.input=input}/>;
    }
}