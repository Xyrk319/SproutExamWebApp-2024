// EmployeeForm.js
import React from 'react';

const EmployeeForm = ({ fullName, birthdate, tin, employeeTypeId, loadingSave, errors, handleChange, handleSubmit, onCancel }) => {

    return (
        <form onSubmit={handleSubmit}>
            <div className='form-row'>
                <div className='form-group col-md-6'>
                    <label htmlFor='inputFullName'>Full Name: *</label>
                    <input type='text' className='form-control' id='inputFullName' onChange={handleChange} name="fullName" value={fullName} placeholder='Full Name' />
                    {errors && errors.FullName && <div className="text-danger">{errors.FullName}</div>}
                </div>
                <div className='form-group col-md-6'>
                    <label htmlFor='inputBirthdate'>Birthdate: *</label>
                    <input type='date' className='form-control' id='inputBirthdate' onChange={handleChange} name="birthdate" value={birthdate !== null ? (new Date(birthdate).toLocaleDateString('fr-CA')) : null} placeholder='Birthdate' />
                    {errors && errors.Birthdate && <div className="text-danger">{errors.Birthdate}</div>}
                </div>
            </div>
            <div className="form-row">
                <div className='form-group col-md-6'>
                    <label htmlFor='inputTin'>TIN: *</label>
                    <input type='text' className='form-control' id='inputTin' onChange={handleChange} value={tin} name="tin" placeholder='TIN' />
                    {errors && errors.Tin && <div className="text-danger">{errors.Tin}</div>}
                </div>
                <div className='form-group col-md-6'>
                    <label htmlFor='inputEmployeeType'>Employee Type: *</label>
                    <select id='inputEmployeeType' onChange={handleChange} value={employeeTypeId} name="employeeTypeId" className='form-control'>
                        <option value='1'>Regular</option>
                        <option value='2'>Contractual</option>
                    </select>
                    {errors && errors.employeeTypeId && <div className="text-danger">{errors.employeeTypeId}</div>}
                </div>
            </div>
            <button type="submit" disabled={loadingSave} className="btn btn-primary mr-2">{loadingSave ? "Loading..." : "Save"}</button>
            <button type="button" onClick={onCancel} className="btn btn-primary">Back</button>
        </form>
    );
}

export default EmployeeForm;
