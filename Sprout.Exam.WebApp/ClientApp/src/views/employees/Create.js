import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';
import EmployeeForm from '../../components/EmployeeForm';

export class EmployeeCreate extends Component {
  static displayName = EmployeeCreate.name;

  constructor(props) {
    super(props);
    this.state = { fullName: null,birthdate: null,tin: null,employeeTypeId: 1, loading: false,loadingSave:false };
  }

  componentDidMount() {
  }

  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value || null});
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
        <h1 id="tabelLabel" >Employee Create</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
      this.setState({ loadingSave: true, errors: {} });
      const token = await authService.getAccessToken();
      const requestOptions = {
          method: 'POST',
          headers: !token ? {} : { 'Authorization': `Bearer ${token}`, 'Content-Type': 'application/json' },
          body: JSON.stringify(this.state)
      };
      const response = await fetch('api/employees', requestOptions);
      
      if (response.status === 201) {
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
      } else {
          const data = await response.json();
          this.setState({ loadingSave: false, errors: data.errors || {} });
      }
  }
}
