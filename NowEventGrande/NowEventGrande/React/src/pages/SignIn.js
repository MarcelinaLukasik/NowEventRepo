import '../styles/account.css';
import React from 'react';
import { useState} from "react";
import Modal from 'react-bootstrap/Modal';
import { useNavigate } from "react-router-dom";

function SignIn() {
    const [email, setEmail] = useState("ADFSD@GMAIL.COM");
    const [password, setPassword] = useState("password");
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleOpen = () => setShow(true);
    const [isLogged, setIsLogged] = useState(false);
    const navigate = useNavigate();

    function handleLogin(evt){
        evt.preventDefault();
        async function fetchData() {
          const res = await fetch(`/account/Login`, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({FirstName: "John", LastName: "Doe", Email: email, Password: password, UserName: "user8"}) ,
          })
          .then(res => res.json()
          .then(localStorage.setItem("user", JSON.stringify(res.data))))
          .then(setIsLogged(true))
          .then(handleClose())           
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

    async function handleSignOut(){
      const res = await fetch(`/account/Logout`, {
        method: "POST"
      });
      if (res.ok) {
        setIsLogged(false);
        navigate(`/`);
      }
    }

    return (
        <div>
            {!isLogged && <button onClick={handleOpen}>SignIn</button>}
            {isLogged && <button onClick={handleSignOut}>SignOut</button>}
            <Modal show={show} onHide={handleClose} className="signInModal">
            <Modal.Header >
              <Modal.Title>Sign in</Modal.Title>
              </Modal.Header>
              <Modal.Body>
              <form className="loginForm" method="post">     
                <hr />
                <div asp-validation-summary="ModelOnly" className="text-danger"></div>
                <div class="form-floating">
                    <input asp-for="Input.Email" className="registerInput" autoComplete="username" aria-required="true" placeholder="Email"/>
                    <label asp-for="Input.Email"></label>
                    <span asp-validation-for="Input.Email" className="text-danger"></span>
                </div>
                <div class="form-floating">
                    <input asp-for="Input.Password" className="registerInput" autoComplete="new-password" aria-required="true" placeholder="Password"/>
                    <label asp-for="Input.Password"></label>
                    <span asp-validation-for="Input.Password" className="text-danger"></span>
                </div>
                <button id="registerSubmit" type="submit" className="registerButton" onClick={handleLogin}>Sign in</button>
            </form>
              </Modal.Body>
          </Modal>
        </div>
    )
}

export default SignIn;