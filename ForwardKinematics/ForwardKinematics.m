clear
Phi1 = @(q1)[cos(q1) -sin(q1) 0;sin(q1) cos(q1) 0;0 0 1];
Phi2 = @(q2)[cos(q2) 0 sin(q2);0 1 0;-sin(q2) 0 cos(q2)];
Phi3 = @(q3)[cos(q3) 0 -sin(q3);0 1 0;sin(q3) 0 cos(q3)];
Phi4 = @(q4)[1 0 0;0 cos(q4) sin(q4);0 -sin(q4) cos(q4)];
Phi5 = @(q5)[cos(q5) 0 -sin(q5);0 1 0;sin(q5) 0 cos(q5)];
Phi6 = @(q6)[1 0 0;0 cos(q6) sin(q6);0 -sin(q6) cos(q6)];

l1 = [50;0;330];
l2 = [0;0;440];
l3 = [100;0;35];
l4 = [420-l3(1);0;0];
l5 = [80;0;0];
l6 = [150;0;0];

q = [12;0;13;5;0;4];

q(3)=q(3)+q(2);

for k=1:6
    q(k) = deg2rad(q(k));
end

x0 = [0;0;0];
x1 = Phi6(q(6))*(l6+x0);
x2 = Phi5(q(5))*(l5+x1);
x3 = Phi4(q(4))*(l4+x2);
x4 = Phi3(q(3))*(l3+x3);
x5 = Phi2(q(2))*(l2+x4);
x6 = Phi1(q(1))*(l1+x5);

Phi01 = Phi1(q(1));
Phi02 = Phi01 * Phi2(q(2));
Phi03 = Phi02 * Phi3(q(3));
Phi04 = Phi03 * Phi4(q(4));
Phi05 = Phi04 * Phi5(q(5));
Phi06 = Phi05 * Phi6(q(6));

joint1 = x6 - Phi01 * (l1 + x5);
joint2 = x6 - Phi02 * (l2 + x4);
joint3 = x6 - Phi03 * (l3 + x3);
joint4 = x6 - Phi04 * (l4 + x2);
joint5 = x6 - Phi05 * (l5 + x1);
joint6 = x6 - Phi06 * (l6 + x0);

%Alternativ -> effizienter. 1 Matrix weniger, weniger Additionen
% joint11 = x6 - x6;
% joint12 = x6 - Phi01 * x5;
% joint13 = x6 - Phi02 * x4;
% joint14 = x6 - Phi03 * x3;
% joint15 = x6 - Phi04 * x2;
% joint16 = x6 - Phi05 * x1;

x6