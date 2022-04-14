import { Contact } from '../models/contact';

class ContactsService {

  APP_API_URL = "http://localhost:5209/api/contacts";

  constructor() {

  }

  getContacts = async (): Promise<Contact[]> => {
    const response = await fetch(this.APP_API_URL);

    const result = await response.json();

    return result;
  }
}

export default new ContactsService();