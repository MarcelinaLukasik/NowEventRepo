import '../styles/account.css';
import React from 'react';
import { useState} from "react";

function Registration() {
    const [fullName, setFullName] = useState("John");
    const [email, setEmail] = useState("Dee");
    const [password, setPassword] = useState("password");

    function handlePost(evt){
        evt.preventDefault();
        async function fetchData() {
          const res = await fetch(`/account/Register`, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({FirstName: "John", LastName: "Doe", Email: email, Password: password, UserName: "qqq"}) ,
          })
          // .then(window.location.reload(false));
        //   if (!res.ok) {
        //     const message = `An error has occured: ${res.status} - ${res.statusText}`;
        //     setValid(false);
        //     throw new Error(message);
        //   }
        //   else{
        //     const result = await res.json();
        //     setValid(true);
        //     addGuestCount();
        //     addChecklistCount();
        //   }
        
      }  
        fetchData();    
      }

    return (

        <div className="event">
            <div >
                <h2>Create a new account</h2>
            </div>
            <form className="registerForm" method="post">     
                <hr />
                <div asp-validation-summary="ModelOnly" className="text-danger"></div>
                <div class="form-floating">
                    <input asp-for="Input.Email" className="registerInput" autoComplete="username" aria-required="true" placeholder="Email"/>
                    <label asp-for="Input.Email"></label>
                    <span asp-validation-for="Input.Email" className="text-danger"></span>
                </div>
                <div className="form-floating">
                    <input asp-for="Input.FullName" className="registerInput" autoComplete="username" aria-required="true" placeholder="Full Name"/>
                    <label asp-for="Input.FullName"></label>
                    <span asp-validation-for="Input.FullName" className="text-danger"></span>
                </div>
                <div class="form-floating">
                    <input asp-for="Input.Password" className="registerInput" autoComplete="new-password" aria-required="true" placeholder="Password"/>
                    <label asp-for="Input.Password"></label>
                    <span asp-validation-for="Input.Password" className="text-danger"></span>
                </div>
                <div class="form-floating">
                    <input asp-for="Input.ConfirmPassword" className="registerInput" autoComplete="new-password" aria-required="true" placeholder="Confirm password"/>
                    <label asp-for="Input.ConfirmPassword"></label>
                    <span asp-validation-for="Input.ConfirmPassword" className="text-danger"></span>
                </div>
                <button id="registerSubmit" type="submit" className="registerButton" onClick={handlePost}>Register</button>
            </form>
        </div>
    )
}

export default Registration;



