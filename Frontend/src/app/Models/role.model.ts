export interface Role {
  id: number;
  name: string;
}

export const constRoles: Role[] = [
  { id: 0, name: 'Customer' },
  { id: 1, name: 'Admin' },
];
