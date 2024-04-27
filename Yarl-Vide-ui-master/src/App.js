import './App.css';
import { Component } from "react";
class App extends Component{
  constructor(props){
    super(props);
    this.state={
      datas:[]
      
    }
  }
  API_URL ="http://localhost:5264/";
  componentDidMount(){
    this.refreshData();
    
  }
  async refreshData(){
    
    fetch(this.API_URL+"api/YarlVibeControllers/GetData").then(response=>response.json()).then(data=>{
      this.setState({datas:data});
    })
  }
  async addClick(event){
    
    var staffName = event.target.newStaffName.value;
    var kitchenId = event.target.newkitchenId.value;
    const data = new FormData();
    data.append("staffName",staffName);
    data.append("kitchenId",kitchenId);
    
    fetch(this.API_URL+"api/YarlVibeControllers/AddData",{
      method:"POST",
      body:data
    }).then(res=>res.json()).then((result)=>{
      alert(result);
      this.refreshData();
    })
  }
  async deleteClick(id){
    
    fetch(this.API_URL+"api/YarlVibeControllers/DeleteData?id="+id,{
      method:"DELETE",
    }).then(res=>res.json()).then((result)=>{
      alert(result);
      this.refreshData();
    })
  }
  render() {
    const{datas} = this.state;
    return (
      <div className="App">
        <h2>
          Yarl Vibe
        </h2>
        <form onSubmit={(event)=>this.addClick(event)}>
        <input
          type="text"
          id="newStaffName"
          placeholder="Enter staff name"
        />
        <br />
        <input
          type="number"
          id="newkitchenId"
          placeholder="Enter kitchen ID"
        />
        <br />
        <button type="submit">Add Details</button>
      </form>
        
        {datas.map(note=><p>
          <b>* {note.StaffName}</b>
          <button onClick={()=>this.deleteClick(note.KitchenStaffID)}>Delete Data</button>
        </p>)}
      </div>
    );
  }
}



export default App;
