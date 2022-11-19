import { useEffect, useState } from "react";
import { Outlet, Link } from "react-router-dom";
import { Container, Row, Col, Nav } from "react-bootstrap"
import { ArrowRightCircle } from "react-bootstrap-icons";
import partyPeopleIcon from '../images/partyImg.png'
import '../styles/banner.css';


const Slider = () => {
    const [loopNum, setLoopNum] = useState(0);
    const [isVisable, setIsVisable] = useState(false);
    const wordsToRotate = ["Simple", "HelpFull", "For you..."];
    const [text, setText] = useState('');
    const [delta, setDelta] = useState(500 - Math.random() * 100);
    const period = 2500;

    useEffect(() => {
        let tic = setInterval(() => {
            tick();
        }, delta)

        return () => { clearInterval(tic) };
    })
    const tick = () => {
        let i = loopNum % wordsToRotate.length;
        let fullText = wordsToRotate[i];
        let textUpdated = isVisable ? fullText.substring(0, text.length - 1) : fullText.substring(0, text.length + 1);

        setText(textUpdated);
        if (isVisable) {
            setDelta(prevD => prevD / 2)
        }

        if (!isVisable && textUpdated === fullText) {
            setIsVisable(true)
            setDelta(period);
        } else if (isVisable && textUpdated === '') {
            setIsVisable(false);
            setLoopNum(loopNum + 1);
            setDelta(500);
        }
    }

    return (
        <section className="slider">
            <Container>
                <Row className="items-center">
                    <Col cs={12} md={6} xl={7}>
                        <h1 className="event-title txt-rotate">{'NowEvent is '}<span className="wrapper">{text}</span></h1>
                        <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
                        <Nav.Link as={Link} to="/event">
                            <button>Get Started <ArrowRightCircle size={25} /></button>
                        </Nav.Link>
                    </Col>
                    <Col cs={12} md={6} xl={5}>
                        <img className="img2" src={partyPeopleIcon} alt="img" />
                    </Col>
                </Row>
            </Container >
        </section >
    )
}

export default Slider;