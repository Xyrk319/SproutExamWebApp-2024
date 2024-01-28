import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import EmployeeForm from '../../components/EmployeeForm';

export class EmployeeEdit extends Component {
  static displayName = EmployeeEdit.name;

  constructor(props) {
    super(props);
    this.state = { id: 0,fullName: '',birthdate: '',tin: '',employeeTypeId: 1, loading: true,loadingSave:false };
  }

  componentDidMount() {
    this.getEmployee(this.props.match.params.id);
  }
  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      if (window.confirm("Are you sure you want to save?")) {
        this.saveEmployee();
      } 
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <EmployeeForm
        fullName={this.state.fullName}
        birthdate={this.state.birthdate}
        tin={this.state.tin}
        employeeTypeId={this.state.employeeTypeId}
        loadingSave={this.state.loadingSave}
        errors={this.state.errors}
        handleChange={this.handleChange.bind(this)}
        handleSubmit={this.handleSubmit.bind(this)}
        onCancel={() => this.props.history.push("/employees/index")}
    />
</div>;


    return (
        <div>
        <h1 id="tabelLabel" >Employee Edit</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true });
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'PUT',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
        body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees/' + this.state.id,requestOptions);

    if(response.status === 200){
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
    }
    else{
        const data = await response.json();
        this.setState({ loadingSave: false, errors: data.errors || {} });
    }
  }

  async getEmployee(id) {
    this.setState({ loading: true,loadingSave: false });
    const token = await authService.getAccessToken();
    const response = await fetch('api/employees/' + id, {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const data = await response.json();
    this.setState({ id: data.id,fullName: data.fullName,birthdate: data.birthdate,tin: data.tin,employeeTypeId: data.employeeTypeId, loading: false,loadingSave: false });
  }
}
