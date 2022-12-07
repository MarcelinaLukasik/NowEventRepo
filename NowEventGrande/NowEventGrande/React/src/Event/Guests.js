import SideBar from "./SideBar";
import React from 'react';
import AllGuests from "./AllGuests";
import '../styles/guests.css';
import '../styles/toggleSwitch.css';
import {Col} from "react-bootstrap";
import calendarIcon from '../images/icons/contact-list.png';
import {useLocation} from 'react-router-dom';
import { useState, useEffect } from "react";
import {handleStyle} from "./HandleProgress";

function Guests() {
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [count, setChecklistCount] = useState(0);
    const [checked, setChecked] = useState(false);
    const toggle = () => {setChecked(!checked); console.log(checked)};

    const addChecklistCount =() => {
        setChecklistCount(count +1);
      }

    const subtractChecklistCount =() => {
    setChecklistCount(count -1);
    }      

    useEffect(() =>{  
        fetchProgress();
    }, [])

    useEffect(() => {
      fetchProgress();
    }, [count]);

    useEffect(() => {
        changeSize();
    }, [checked]);

    async function fetchProgress() {
      const res = await fetch(`/events/${eventId}/GetChecklistProgress`);      
      res
        .json()
        .then(res => {
            setChecklistCount(res);
            handleStyle(res);
        })                
    }

    async function changeSize(){
        let choosenSize = "Small";
        if (checked){
            choosenSize = "Large";
        }
        else{
            choosenSize = "Small";
        }

        const res = await fetch(`/events/${id}/SetSize`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "x-access-token": "token-value",
        },
        body: JSON.stringify(choosenSize),
        })
        
        if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            // setValid(false);
            throw new Error(message);
            }
            // else{
            // setValid(true);
            // fetchProgress();
            // }
        // } 
    }

    async function changeSizeRange(event){
        let sizeRange = event.target.value;
        const res = await fetch(`/events/${id}/SetSizeRange`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "x-access-token": "token-value",
        },
        body: JSON.stringify(sizeRange),
        })
        
        if (!res.ok) {
            const message = `An error has occured: ${res.status} - ${res.statusText}`;
            // setValid(false);
            throw new Error(message);
            }
        //     else{
        //     setValid(true);
        //     fetchProgress();
        //     }
        // } 
    }
    

    return (
        <div className="event">
            <div>  
                <div className="row">
                    <div className="Event-col-12">    
                        <div className="progressBarContainer"> 
                            <h3 className="progressText" >Checklist progress:</h3>  
                                <div className="progress" id="progress">
                                    <div className="progress-bar" id="progress-bar"></div>
                                </div> 
                        </div>
                    </div>
                </div>     
                <h1>Guests list</h1>
                <div className="row">
                    <div className="Event-col-3">
                    <div className="sidebar">
                        <SideBar eventId={eventId}/>
                    </div>
                    </div>        
                   <div className="Event-col-5 guestList">              
                        <br/> 
                        {checked && 
                        <div>
                        {/* <p>Big event</p> */}
                        <h3>Choose your event range:</h3>
                       
                        <input type="button" className="saveRange" value="Less than 100" onClick={changeSizeRange}/>
                        <input type="button" className="saveRange" value="From 100 to 500" onClick={changeSizeRange}/>
                        <input type="button" className="saveRange" value="More than 500" onClick={changeSizeRange}/>
                       
                        </div>
                        } 
                        {!checked && 
                        <div>
                            {/* <p>Small event</p>  */}
                            <div className="guestList">
                                <AllGuests eventId={id} addChecklistCount={addChecklistCount} subtractChecklistCount={subtractChecklistCount}/>
                            </div>
                        </div> }                                                               
                                              
                   </div>
                   <div className="Event-col-3">                           
                    <Col cs={12} md={6} xl={6}>
                    <div className="rangeCol">
                        <h3>Large-scale event:</h3>
                        <label className="switch">
                        <input type="checkbox"
                            id="toggleSwitch"
                            checked={checked}
                            onChange={toggle} 
                        />
                        <span className="toggle round"></span>
                        </label> 
                        </div>
                        <img src={calendarIcon} alt="img" className="featureIcon"/>
                    </Col>
                    </div>                  
                </div>                             
            </div>
        </div>
)
}

export default Guests;