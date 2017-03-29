
interface AppVerbProps {
    present: string,
    past: string,
    perfect: string,

    show_present: boolean,
    show_past: boolean,
    show_perfect: boolean    
}

class AppVerb extends React.Component<AppVerbProps,{}> {

   answer() {
        alert(1)
   }

   render() {
      return (
         <div>
            <form autoFocus>
             <div className="verbtable col-md-8 col-md-offset-2">
                <div className="row">
                    <span className="col-md-4"><VerbText ref="p1" show={this.props.show_present} label="Present" answer={this.props.present}/></span>
                    <span className="col-md-4"><VerbText ref="p2" show={this.props.show_past} label="Past" answer={this.props.past}/></span>
                    <span className="col-md-4"><VerbText ref="p3" show={this.props.show_perfect} label="Present Perfect" answer={this.props.perfect}/></span>
                </div>                
            </div>

            <div className="verbsubmit col-md-12">
            <button type="submit" className="btn btn-lg btn-primary" onClick={this.answer}>Submit</button>
                
            </div>
           </form>
         </div>
      );
   }
}

interface VerbTextProps {
    answer: string;
    label: string;
    show: boolean;
}

class VerbText extends React.Component<VerbTextProps,{}> {
    render() {

        let showAnswer = this.props.show;
        let label = this.props.label;
        let verb = this.props.answer;

        if(showAnswer) {
            return <p className="form-control-static"><strong>{ verb }</strong></p>;            
        }

        return <input className="form-control text-center" type="text" placeholder={label} />;
    }
}