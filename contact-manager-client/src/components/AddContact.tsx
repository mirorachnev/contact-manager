import React, { Component } from 'react';
import { Button, Form, Modal, } from 'react-bootstrap';
import { ActionModelDialogProps } from '../models/actionModel';
import contactsService from '../services/contactsService';
import { Contact } from '../models/contact';

export class AddContact extends Component<ActionModelDialogProps> {  

  constructor(props: ActionModelDialogProps) {
    super(props);   
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
      id: ''
    };

    await contactsService.createContact(contact);

    this.props.reloadData();
    this.props.onHide();
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
              Add Contact
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>

            <Form onSubmit={(e) => this.handleOnSubmit(e)}>
                <Form.Group>
                <Form.Label>First Name</Form.Label>
                    <Form.Control type="text" name="FirstName" required
                      placeholder="First Name" />
              </Form.Group>
                   
              <Form.Group>
                <Form.Label>Last Name</Form.Label>
                <Form.Control type="text" name="LastName" required
                  placeholder="Last Name" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Email</Form.Label>
                <Form.Control type="text" name="Email" required
                  placeholder="Email" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Phone Number</Form.Label>
                <Form.Control type="text" name="PhoneNumber" required
                  placeholder="Phone Number" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Date of Birth</Form.Label>
                <Form.Control type="text" name="DateOfBirth" required
                  placeholder="Date of Birth" />
              </Form.Group>
              <Form.Group>
                <Form.Label>Address</Form.Label>
                <Form.Control type="text" name="Address" required
                  placeholder="Address" />
              </Form.Group>
              <Form.Group>
                <Form.Label>IBAN</Form.Label>
                <Form.Control type="text" name="Iban" required
                  placeholder="Iban" />
              </Form.Group>
              <Form.Group>
                <Button variant="primary" type="submit">
                    Save
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