import React, { useState } from 'react';
import { Col, Container, Row } from 'react-bootstrap';
import envelopeImg from "../images/envelope.svg";
import '../styles/contact.css';


export const Contact = () => {
    const initialDeatils = {
        FirstName: '',
        LastName: '',
        EmailAddress: '',
        Subject: '',
        PhoneNumber: '',
        Message: '',
    }
    const [formDetails, setFormDetails] = useState(initialDeatils);
    const [buttonSubmit, setButtonSubmit] = useState('Send');
    const [status, setStatus] = useState({});

    const onFormUpdate = (category, value) => {
        console.log(value)
        setFormDetails({
            ...formDetails,
            [category]: value
        })
        console.log(formDetails);
    }
    //TODO: refactor for email sending
    async function handleSubmit(evt) {
        evt.preventDefault();
        setButtonSubmit("Sending...")
        console.log(formDetails.FirstName);
        const response = await fetch('/email/SendEmail', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify({ FirstName: formDetails.FirstName, LastName: formDetails.LastName, EmailAddress: formDetails.EmailAddress, Subject: formDetails.Subject, PhoneNumber: formDetails.PhoneNumber, Message: formDetails.Message })
            // body: JSON.stringify({ formDetails })
        });
        setButtonSubmit("Send");
        if (response.status === 200) {
            console.log("imhere200");
            setStatus({ success: true, message: "Email sent successfully!" })
        } else {
            console.log("imhere no200");
            setStatus({ success: false, message: "Something went wrong, please try again..." })
        }
        setFormDetails(initialDeatils)
    }

    return (
        <section className="contact" id="contact">
            <Container>
                <Row >
                    <Col md={6} className='contact-row'>
                        <img className='envImg' src={envelopeImg} alt="contact" />
                    </Col>
                    <Col md={6}>
                        <h2>Get in Touch</h2>
                        <form onSubmit={handleSubmit}>
                            <Row>
                                <Col sm={6}>
                                    <input type="text" value={formDetails.FirstName} placeholder="First Name..." onChange={(e) => onFormUpdate('FirstName', e.target.value)} />
                                </Col>
                                <Col sm={6}>
                                    <input type="text" value={formDetails.LastName} placeholder="Last Name..." onChange={(e) => onFormUpdate('LastName', e.target.value)} />
                                </Col>
                                <Col sm={6}>
                                    <input type="text" value={formDetails.EmailAddress} placeholder="Email..." onChange={(e) => onFormUpdate('EmailAddress', e.target.value)} />
                                </Col>
                                <Col sm={6}>
                                    <input type="text" value={formDetails.PhoneNumber} placeholder="Phone..." onChange={(e) => onFormUpdate('PhoneNumber', e.target.value)} />
                                </Col>
                                <Col sm={12}>
                                    <input row="6" type="text" value={formDetails.Subject} placeholder="Subject..." onChange={(e) => onFormUpdate('Subject', e.target.value)} />
                                </Col>
                                <Col>
                                    <textarea row="6" value={formDetails.Message} placeholder="Message..." onChange={(e) => onFormUpdate('Message', e.target.value)}></textarea>
                                    <button type='submit'><span>{buttonSubmit}</span></button>
                                </Col>
                            </Row>
                            {
                                status.message &&
                                <Col>
                                    <p className={status.success === false ? "danger" : "success"}>{status.message}</p>
                                </Col>
                            }
                        </form>
                    </Col>
                </Row>
            </Container>
        </section >
    )
}
