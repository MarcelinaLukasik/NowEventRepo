import { useEffect, useState } from "react";
import { Outlet, Link } from "react-router-dom";
import { Container, Row, Col, Nav } from "react-bootstrap";
import { ArrowRightCircle } from "react-bootstrap-icons";
import partyPeopleIcon from "../images/partyImg.png";
import "../styles/banner.css";

const Slider = () => {
  const [loopNum, setLoopNum] = useState(0);
  const [isVisable, setIsVisable] = useState(false);
  const wordsToRotate = ["Simple", "HelpFull", "For you..."];
  const [text, setText] = useState("");
  const [delta, setDelta] = useState(500 - Math.random() * 100);
  const period = 2500;

  useEffect(() => {
    let tic = setInterval(() => {
      tick();
    }, delta);

    return () => {
      clearInterval(tic);
    };
  });
  const tick = () => {
    let i = loopNum % wordsToRotate.length;
    let fullText = wordsToRotate[i];
    let textUpdated = isVisable
      ? fullText.substring(0, text.length - 1)
      : fullText.substring(0, text.length + 1);

    setText(textUpdated);
    if (isVisable) {
      setDelta((prevD) => prevD / 2);
    }

    if (!isVisable && textUpdated === fullText) {
      setIsVisable(true);
      setDelta(period);
    } else if (isVisable && textUpdated === "") {
      setIsVisable(false);
      setLoopNum(loopNum + 1);
      setDelta(500);
    }
  };

  return (
    <section className="mainSlider">
      <Container>
        <Row className="items-center">
          <Col cs={12} md={6} xl={7}>
            <h1 className="event-title txt-rotate">
              {"NowEvent is "}
              <span className="wrapper">{text}</span>
            </h1>
            <p>
              Welcome to NowEvent! We're glad that you're here. We know that
              plannig events is not an easy task, and requires a lot of free
              time. NowEvent was created to help people create their events in a
              fast and easy way. All you need to do is sign up and choose all
              the details, and don't forget to have fun! :)
            </p>
            <Nav.Link as={Link} to="/event">
              <button className="createEventButton">
                Get Started <ArrowRightCircle size={25} />
              </button>
            </Nav.Link>
          </Col>
          <Col cs={12} md={6} xl={5}>
            <img className="img2" src={partyPeopleIcon} alt="img" />
          </Col>
        </Row>
      </Container>
    </section>
  );
};

export default Slider;
