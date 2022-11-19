import React from 'react';
import { useState } from "react";

function AddGuestForm({onClick, isOpen, eventId}) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
   
    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost();
        isOpen = false;
        onClick();
        setFirstName("");
      }

    function handlePost(){
      async function fetchData() {
        const res = await fetch(`/events/GetGuest`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({FirstName: firstName, LastName: lastName, Email: email, EventId: eventId}) ,
        })
        .then(window.location.reload(false));
        if (!res.ok) {
          const message = `An error has occured: ${res.status} - ${res.statusText}`;
          throw new Error(message);
        }
    }  
      fetchData();    
    }
    
    return (   
        <div className='addGuestForm'>
        <button className="addButton" onClick={onClick}>Add guest</button>
        {isOpen &&
        <container className="addGuestContainer">
        <form onSubmit={handleSubmit}>

            <label>First name:</label>
            <input className="addGuest" type="text" value={firstName} required onChange={e => setFirstName(e.target.value)}/>

            <label>Last name:</label>
            <input className="addGuest" type="text" value={lastName} required onChange={e => setLastName(e.target.value)}/>

            <label>Email:</label>
            <input className="addGuest" type="email" value={email} required onChange={e => setEmail(e.target.value)}/>
            <input className="saveGuest" type="submit" value="Save"/>
        </form>
        </container>
        }
        </div>   
    )
      
}

export default AddGuestForm;