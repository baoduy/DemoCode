import { PostApi } from '../api';

describe('Test POST APIs', () => {
  test('Test GET POSTS', async () => {
    const result = await PostApi.get();
    expect(result.data.length).toBeGreaterThanOrEqual(100);
  });

  test('Test GET POSTS with query parameters', async () => {
    const result = await PostApi.get({ userId: 1 });
    expect(result.data.length).toBeGreaterThanOrEqual(10);
  });

  test('Test GET POST by Id', async () => {
    const result = await PostApi.getById(1);
    expect(result.data.id).toBe(1);
  });

  test('Test GET POSTS Comments', async () => {
    const result = await PostApi.getComments(1);
    expect(result.data.length).toBeGreaterThanOrEqual(5);
  });

  test('Create a new POST', async () => {
    const result = await PostApi.post({
      id: 0, //The Id is not important as it is generating by Server.
      userId: 1,
      title: 'Duy Hoang',
      body: 'Duy Hoang'
    });

    expect(result.data.id).toBeGreaterThanOrEqual(1);
    expect(result.data.title).toBe('Duy Hoang');
    expect(result.data.body).toBe('Duy Hoang');
  });

  test('Update existing POST', async () => {
    const result = await PostApi.put({
      id: 1, //The Id is important to Update existing post.
      title: 'Duy Hoang',
      body: 'Duy Hoang'
    });

    expect(result.data.title).toBe('Duy Hoang');
    expect(result.data.body).toBe('Duy Hoang');
  });

  test('Delete existing POST', async () => {
    const result = await PostApi.delete(1);
    expect(result.data).toMatchObject({});
  });
});
