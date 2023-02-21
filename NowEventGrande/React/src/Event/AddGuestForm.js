import React from 'react';
import { useState} from "react";

function AddGuestForm({onClick, addGuestCount, isOpen, eventId, addChecklistCount}) {
    const [firstName, setFirstName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [isValid, setValid] = useState(true);
  
    function handleSubmit(evt) {
        evt.preventDefault();
        handlePost();
        isOpen = false;
        onClick();
        setFirstName("");
      }

    function handlePost(){
      async function fetchData() {
        const res = await fetch(`/guest/SaveGuest`, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({FirstName: firstName, LastName: lastName, Email: email, EventId: eventId}) ,
        })
        if (!res.ok) {
          const message = `An error has occured: ${res.status} - ${res.statusText}`;
          setValid(false);
          throw new Error(message);
        }
        else{
          const result = await res.json();
          setValid(true);
          addGuestCount();
          addChecklistCount();
        }
      
    }  
      fetchData();    
    }
    
    return (   
        <div className='addGuestForm'>
          
        <button className="addButton" onClick={onClick}>Add guest</button>
        {isOpen &&
        <div className="addGuestContainer">
        <form onSubmit={handleSubmit}>

            <label>First name:</label>
            <input className="addGuest" type="text" value={firstName} required onChange={e => setFirstName(e.target.value)}/>

            <label>Last name:</label>
            <input className="addGuest" type="text" value={lastName} required onChange={e => setLastName(e.target.value)}/>

            <label>Email:</label>
            <input className="addGuest" type="email" value={email} required onChange={e => setEmail(e.target.value)}/>
            <input className="saveGuest" type="submit" value="Save"/>
        </form>
        </div>
        }
        <div>
          {!isValid && <p className="wrongInputMessage">Please provide valid name and email!</p>}
        </div>   
        </div>   
    )
      
}

export default AddGuestForm;