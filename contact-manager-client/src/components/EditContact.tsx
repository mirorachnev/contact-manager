import React, { Component } from 'react';
import { Button, ButtonToolbar, Col, Form, Modal, Row, Table } from 'react-bootstrap';
import { ActionModelDialogProps } from '../models/actionModel';
import contactsService from '../services/contactsService';
import { Contact } from '../models/contact';

export class EditContact extends Component<ActionModelDialogProps> {

  state: { id: string, contact: Contact };

  constructor(props: ActionModelDialogProps) {
    super(props);

    this.state = {
      id: this.props.id, contact: {
        firstName: '',
        lastName: '',
        email: '',
        phoneNumber: '',
        address: '',
        dateOfBirth: '',
        iban: '',
        id: ''
      }
    };
  }

  handleOnSubmit = async (ev: any) => {

    ev.preventDefault();
    const contact: Contact = {
      firstName: ev.target.FirstName.value,
      lastName: ev.target.LastName.value,
      email: ev.target.Email.value,
      phoneNumber: ev.target.PhoneNumber.value,
      address: ev.target.Address.value,
      dateOfBirth: ev.target.DateOfBirth.value,
      iban: ev.target.Iban.value,
      id: this.props.id
    };

    await contactsService.updateContact(contact);

    this.props.reloadData();
    this.props.onHide();
  }

  componentDidMount = async () => {
    const contact = await contactsService.getContact(this.props.id);

    this.setState({
      contact: {
        firstName: contact.firstName,
        lastName: contact.lastName,
        email: contact.email,
        phoneNumber: contact.phoneNumber,
        address: contact.address,
        dateOfBirth: contact.dateOfBirth,
        iban: contact.iban,
        id: contact.id
      } })
  }

  render() {
    return (
      <>
        <Modal
          {...this.props}
          size="lg"
          aria-labelledby="contained-modal-title-vcenter"
          centered
        >
          <Modal.Header closeButton>
            <Modal.Title id="contained-modal-title-vcenter">
              Edit Contact
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>

            <Form onSubmit={(e) => this.handleOnSubmit(e)}>
              <Form.Group>
                <Form.Label>First Name</Form.Label>
                <Form.Control type="text" name="FirstName" required
                  defaultValue={this.state.contact.firstName}
                  placeholder="First Name" />
              </Form.Group>

              <Form.Group>
                <Form.Label>Last Name</Form.Label>
                <Form.Control type="text" name="LastName" required
                  defaultValue={this.state.contact.lastName}
                  placeholder="Last Name" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Email</Form.Label>
                <Form.Control type="text" name="Email" required
                  defaultValue={this.state.contact.email}
                  placeholder="Email" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Phone Number</Form.Label>
                <Form.Control type="text" name="PhoneNumber" required
                  defaultValue={this.state.contact.phoneNumber}
                  placeholder="Phone Number" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Date of Birth</Form.Label>
                <Form.Control type="text" name="DateOfBirth" required
                  defaultValue={this.state.contact.dateOfBirth}
                  placeholder="Date of Birth" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Address</Form.Label>
                <Form.Control type="text" name="Address" required
                  defaultValue={this.state.contact.address}
                  placeholder="Address" />
              </Form.Group>
              <Form.Group>
                <Form.Label>IBAN</Form.Label>
                <Form.Control type="text" name="Iban" required
                  defaultValue={this.state.contact.iban}
                  placeholder="Iban" />
              </Form.Group>
              <Form.Group>
                <Button variant="primary" type="submit">
                  Update
                </Button>
              </Form.Group>
            </Form>

          </Modal.Body>

          <Modal.Footer>
            <Button variant="danger" onClick={this.props.onHide}>Close</Button>
          </Modal.Footer>

        </Modal>
      </>);
  }
}