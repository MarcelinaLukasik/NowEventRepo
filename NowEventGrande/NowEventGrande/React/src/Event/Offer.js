import React from 'react';
import '../styles/guests.css';
import { Col } from "react-bootstrap";
import calendarIcon from '../images/icons/find.png';
import { useLocation } from 'react-router-dom';
import { useState, useEffect } from "react";
import SideBar from "./SideBar";

function Offer() {
    const [isOpen, setOpen] = useState(false);
    const openInput = () => { setOpen(!isOpen); }
    const location = useLocation();
    const eventId = location.state.EventId;
    const [id, setEventId] = useState(eventId);
    const [status, setStatus] = useState(false);
    const [buttonSubmit, setButtonSubmit] = useState('Post My Offer');
    const [statusMsg, setStatusMsg] = useState({});

    useEffect(() => {
        async function fetchData() {
            const res = await fetch(`/events/${id}/CheckStatus`);
            res
                .json()
                .then(res => setStatus(res))
        }
        fetchData();
    }, []);

    async function handlePostOffer(e) {
        e.preventDefault();
        setButtonSubmit("Sending...")
        const response = await fetch('/events/PostOffer', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ ClientId: 0, EventId: eventId, Status: "Open" })
        });
        setButtonSubmit("Post My Offer");
        if (response.status === 200) {
            console.log("imhere200");
            setStatus({ success: true, message: "Email sent successfully!" })
        } else {
            console.log("imhere no200");
            setStatus({ success: false, message: "Something went wrong, please try again..." })
        }
    }

    return (
        <div className="event">
                    <div>              
                        <h1>Post your offer</h1>
                        
                        <div className="row">                     
                            <div className="Event-col-3">
                            <div className="sidebar">
                                <SideBar eventId={eventId}/>
                            </div>
                            </div>
                        <div  className="Event-col-6">
                            {status &&
                            <div>
                                <h3 className="offerInformation">Good job, your event checklist is now completed.
                                    Make sure all data is correct before you post your offer.</h3>
                                <button type='submit' className='postOffer' onClick={handlePostOffer}><span>{buttonSubmit}</span></button>
                            </div>
                        }
                        {!status &&
                            <h3 className="offerInformation">Your event is not completed yet. Provide all required information and come back!</h3>
                        }
                    </div>
                    <div className="Event-col-2">
                        <Col cs={12} md={6} xl={6}>
                            <img src={calendarIcon} alt="img" className="featureIcon" />
                        </Col>
                    </div>
                    {
                        statusMsg.message &&
                        <Col>
                            <p className={statusMsg.success === false ? "danger" : "success"}>{statusMsg.message}</p>
                        </Col>
                    }
                </div>
            </div>
        </div>

    )
}

export default Offer;   