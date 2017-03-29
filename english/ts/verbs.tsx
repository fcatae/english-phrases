
class AppVerb extends React.Component<{},{}> {
   render() {
      return (
         <div>
             <div className="verbheader col-md-8 col-md-offset-2">
                <div className="row">
                    <span className="col-md-4">Present</span>
                    <span className="col-md-4">Past</span>
                    <span className="col-md-4"> Present Perfect</span>
                </div>
            </div>

             <div className="verbtable col-md-8 col-md-offset-2">
                <div className="row">
                    <span className="col-md-4">Present</span>
                    <span className="col-md-4">Past</span>
                    <span className="col-md-4"> Present Perfect</span>
                </div>
            </div>

         </div>
      );
   }
}